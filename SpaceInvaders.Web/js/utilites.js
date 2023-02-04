/**
 * Asynchronously reads file selected by user into an
 * ArrayBuffer.
 * 
 * @param {File} file File to be read into ArrayBuffer
 * @returns {Promise} Promise object that represents file ArrayBuffer
 */
export function readFileAsync(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = reject;

        reader.readAsArrayBuffer(file);
    });
}