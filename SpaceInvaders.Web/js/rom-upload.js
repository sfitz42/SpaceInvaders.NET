import { methods } from './main.js'

const selectButton = document.querySelector('#romSelect');
const fileUpload = document.querySelector('#romUpload');

selectButton.addEventListener('click', () => fileUpload.click());
fileUpload.addEventListener('change', processRoms, false);

const uploadedFiles = {
    INVADERS_H: false,
    INVADERS_G: false,
    INVADERS_F: false,
    INVADERS_E: false,
};

/**
 * Asynchronously reads file selected by user into an
 * ArrayBuffer.
 * 
 * @param {File} file File to be read into ArrayBuffer
 * @returns {Promise} Promise object that represents file ArrayBuffer
 */
function readRomAsync(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = reject;

        reader.readAsArrayBuffer(file);
    })
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
        const uploadStatus = uploadedFiles[name];

        if (uploadStatus == true || uploadStatus == undefined)
            continue;

        const data = new Uint8Array(await readRomAsync(file));

        methods.LoadRom(name, data);

        uploadedFiles[name] = true;

        updateList();
    }
}

function updateList() {
    let allUploaded = true;

    for (const [key, value] of Object.entries(uploadedFiles)) {
        if (value) {
            const icon = document.querySelector(`#${key}_Status`);

            icon.classList.remove("bi-exclamation-diamond-fill");
            icon.classList.add("bi-check-lg");
        }
        else {
            allUploaded = false;
        }
    }

    if (allUploaded) {
        selectButton.style.display = 'none';
        
        methods.StartGame();
    }
}