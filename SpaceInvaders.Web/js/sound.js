import { readFileAsync } from './utilites.js'

const selectButton = document.querySelector('#soundSelect');
const soundUpload = document.querySelector('#soundUpload');

selectButton.addEventListener('click', () => soundUpload.click());
soundUpload.addEventListener('change', processSoundFiles, false);

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

export function playSound() {
    if (!soundEnabled)
        return;
}

/**
 * Reads ROM files selected in file input into unsigned
 * byte array buffer.
 * 
 * Returned buffer is then passed to WASM LoadRom method.
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

    updateList();
}

async function processSoundFile(name, file) {
    const data = new Uint8Array(await readFileAsync(file));

    uploadedSoundFiles[name] = data;
}

function updateList() {
    let soundsUploaded = true;

    for (const [key, value] of Object.entries(uploadedSoundFiles)) {
        if (value === null) {
            soundsUploaded = false;
        }
    }

    if (soundsUploaded) {
        soundEnabled = true;

        console.log("All sounds loaded.");
    }

    console.log(uploadedSoundFiles);
}
