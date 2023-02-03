﻿import { methods } from './main.js'

const selectButton = document.querySelector('#romSelect');
const fileUpload = document.querySelector('#romUpload');

selectButton.addEventListener('click', () => fileUpload.click());
fileUpload.addEventListener('change', processRoms, false);

const uploadedRomFiles = {
    INVADERS_H: false,
    INVADERS_G: false,
    INVADERS_F: false,
    INVADERS_E: false,
};

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

/**
 * Asynchronously reads file selected by user into an
 * ArrayBuffer.
 * 
 * @param {File} file File to be read into ArrayBuffer
 * @returns {Promise} Promise object that represents file ArrayBuffer
 */
function readFileAsync(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = reject;

        reader.readAsArrayBuffer(file);
    });
}

/**
 * Reads ROM files selected in file input into unsigned
 * byte array buffer.
 * 
 * Returned buffer is then passed to WASM LoadRom method.
 * 
 * @param {Event} e - Fired HTMLElement change event
 */
async function processRoms(e) {
    const files = e.target.files;

    for (const file of files) {
        const name = file.name.replace('.', '_').toUpperCase();

        const romStatus = uploadedRomFiles[name];
        const soundStatus = uploadedSoundFiles[name];

        if (romStatus !== true && romStatus !== undefined) {
            await processRomFile(name, file);
        }
        else if (soundStatus === null && soundStatus !== undefined) {
            uploadedSoundFiles[name] = new Uint8Array(await readFileAsync(file));
        }

        updateList();
    }
}

async function processRomFile(name, file) {
    const data = new Uint8Array(await readFileAsync(file));

    methods.LoadRom(name, data);

    uploadedRomFiles[name] = true;
}

function updateList() {
    let romsUploaded = true;
    let soundsUploaded = true;

    for (const [key, value] of Object.entries(uploadedRomFiles)) {
        if (value) {
            const icon = document.querySelector(`#${key}_Status`);

            icon.classList.remove("bi-exclamation-diamond-fill");
            icon.classList.add("bi-check-lg");
        }
        else {
            romsUploaded = false;
        }
    }

    for (const [key, value] of Object.entries(uploadedSoundFiles)) {
        if (value === null) {
            soundsUploaded = false;
        }
    }

    if (romsUploaded && soundsUploaded) {
        selectButton.style.display = 'none';
        
        methods.StartGame();
    }
}