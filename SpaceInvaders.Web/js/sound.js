import { readFileAsync } from './utilites.js'

const selectButton = document.querySelector('#soundSelect');
const soundUpload = document.querySelector('#soundUpload');

selectButton.addEventListener('click', () => soundUpload.click());
soundUpload.addEventListener('change', processSoundFiles, false);

let context;

let soundEnabled = false;

const uploadedSoundFiles = {
    "0_WAV": null,
    "1_WAV": null,
    "2_WAV": null,
    "3_WAV": null,
    "4_WAV": null,
    "5_WAV": null,
    "6_WAV": null,
    "7_WAV": null,
    "8_WAV": null,
};

const buffers = [];
let ufoSource = null;

/**
 * Checks if browser supports Web Audio API and 
 * initialises AudioContext.
 * 
 * Uploaded sound files are decoded and added to buffers.
 */
async function init() {
    try {
        context = new AudioContext();
    }
    catch (e) {
        alert('Web Audio API is not supported by this browser');
        return;
    }

    soundEnabled = true;

    for (const [key, value] of Object.entries(uploadedSoundFiles)) {
        const buffer = await context.decodeAudioData(value);

        buffers.push(buffer);
    }
}

/**
 * Triggers specific sound buffer to be played
 * by AudioContext.
 * 
 * Intended to be called by emulator WASM module.
 * 
 * @param {number} soundType 
 */
export function playSound(soundType) {
    if (!soundEnabled)
        return;

    const buffer = buffers[soundType];
    const source = context.createBufferSource();

    // Set sound to loop if UFO sound is selected
    if (soundType == 0) {
        source.loop = true;
        ufoSource = source;
    }
    
    source.buffer = buffer;
    source.connect(context.destination);
    source.start();
}

/**
 * Stops looping UFO sound when emulator triggers
 * end event.
 * 
 * Intended to be called by emulator WASM module.
 */
export function endUFO() {
    if (ufoSource === null)
        return;

    ufoSource.stop();
    ufoSource.disconnect();
    ufoSource = null;
}

/**
 * Reads sound files selected in file input into
 * array buffer.
 * 
 * Returned buffer is then stored to be decoded
 * into audio data by AudioContext.
 * 
 * Will not accept any changes when sound has
 * already been successfully enabled.
 * 
 * @param {Event} e - Fired HTMLElement change event
 */
async function processSoundFiles(e) {
    if (soundEnabled)
        return;

    const files = e.target.files;

    for (const file of files) {
        const name = file.name.replace('.', '_').toUpperCase();

        const soundStatus = uploadedSoundFiles[name];

        if (soundStatus === null && soundStatus !== undefined) {
            await processSoundFile(name, file);
        }
    }

    updateStatus();
}

/**
 * Reads uploaded file into ArrayBuffer and stores
 * the returned buffer in dictionary.
 * 
 * @param {String} name - Name of uploaded sound file 
 * @param {File} file - File object containing uploaded file metadata
 */
async function processSoundFile(name, file) {
    const data = await readFileAsync(file);

    uploadedSoundFiles[name] = data;
}

/**
 * Checks whether all sound files have successfully 
 * been uploaded.
 * 
 * If all keys in dictionary have valid data attached
 * init() is called.
 */
function updateStatus() {
    let soundsUploaded = true;

    for (const [key, value] of Object.entries(uploadedSoundFiles)) {
        if (value === null) {
            soundsUploaded = false;
        }
        else {
            const soundStatus = document.querySelector(`#Status_${key}`);

            if (soundStatus != null) {
                soundStatus.remove();
            }
        }
    }

    if (soundsUploaded) {
        document.querySelector("#RequiredSounds").style.display = 'none';
        selectButton.style.display = 'none';

        init();
    }
}
