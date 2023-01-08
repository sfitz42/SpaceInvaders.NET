import { methods } from './main.js'

const fileUpload = document.querySelector('#romSelect');
fileUpload.addEventListener('change', processRoms, false);

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

        const data = new Uint8Array(await readRomAsync(file));

        methods.LoadRom(name, data);
    }
}
