const width = 256;
const height = 224;

const vertShaderSource = `
attribute vec3 aPos;
attribute vec2 texCoord;

varying highp vec2 uv;

void main() {
    uv = texCoord;
    gl_Position = vec4(aPos, 1.0);
}
`;

const fragShaderSource = `
varying highp vec2 uv;
uniform sampler2D uSampler;

void main() {
    gl_FragColor = texture2D(uSampler, uv);
}
`;

const displayVertices = new Float32Array([
    1.0, 1.0, 0.0, 1.0, 1.0,
    1.0, -1.0, 0.0, 0.0, 1.0,
    -1.0, -1.0, 0.0,  0.0, 0.0,
    -1.0, 1.0, 0.0, 1.0, 0.0
]);

const vertIndices = new Uint16Array([
    0, 1, 3,
    1, 2, 3
]);

const canvas = document.querySelector('#gameDisplay');

canvas.width = height * 2;
canvas.height = width * 2;

/** @type {WebGLRenderingContext} */
const gl = canvas.getContext('webgl');

let vertexBuffer;
let elementBuffer;

function init() {
    initShaders();
    initTexture();
    initBuffers();

    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);
}

export function draw(vram) {
    updateTexture(vram);

    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    gl.drawElements(gl.TRIANGLES, vertIndices.length, gl.UNSIGNED_SHORT, 0);
}

function initBuffers() {
    vertexBuffer = gl.createBuffer();

    gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, displayVertices, gl.STATIC_DRAW);

    elementBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, elementBuffer);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, vertIndices, gl.STATIC_DRAW);

    const vertPosLoc = gl.getAttribLocation(gl.program, 'aPos');

    gl.vertexAttribPointer(vertPosLoc, 3, gl.FLOAT, false, 5 * 4, 0);
    gl.enableVertexAttribArray(vertPosLoc);

    const texPosLoc = gl.getAttribLocation(gl.program, 'texCoord');
    gl.vertexAttribPointer(texPosLoc, 2, gl.FLOAT, false, 5 * 4, 3 * 4);
    gl.enableVertexAttribArray(texPosLoc);
}

function initShaders() {
    const vertexShader = compileShader(vertShaderSource, gl.VERTEX_SHADER);
    const fragmentShader = compileShader(fragShaderSource, gl.FRAGMENT_SHADER);

    const glProgram = gl.createProgram();

    gl.attachShader(glProgram, vertexShader);
    gl.attachShader(glProgram, fragmentShader);
    gl.linkProgram(glProgram);

    gl.useProgram(glProgram);
    gl.program = glProgram;
}

function compileShader(src, type) {
    const shader = gl.createShader(type);

    gl.shaderSource(shader, src);
    gl.compileShader(shader);

    return shader;
}

function initTexture() {
    const pixelData = new Uint8Array(224 * 256 * 4);

    for (let i = 0; i < pixelData.length; i += 4) {
        pixelData[i + 3] = 0xFF;
    }

    const handle = gl.createTexture();

    // use texture
    gl.activeTexture(gl.TEXTURE0);
    gl.bindTexture(gl.TEXTURE_2D, handle);

    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, width, height, 0, gl.RGBA, gl.UNSIGNED_BYTE, pixelData);

    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);

    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.NEAREST);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);

    const fragmentSamplerUniform = gl.getUniformLocation(gl.program, "uSampler");
    gl.uniform1i(fragmentSamplerUniform, 0);
}

function updateTexture(vram) {
    // const vram = Methods.GetVRAM();

    const totalBytes = width * height / 8;

    const pixelData = new Uint8Array(224 * 256 * 4);

    for (let i = 0; i < totalBytes; i++) {
        let currByte = vram[i];

        for (let j = 0; j < 8; j++) {
            const pixelSet = (currByte & (0x01 << j)) != 0;

            const index = ((i * 8) + j) * 4;
            
            const x = i * 8 % 256;
            const y = i * 8 / 256;

            let r, g, b;

            if (pixelSet) {
                // Set default pixel colour (white)
                r = 0xFF;
                g = 0xFF;
                b = 0xFF;

                // Set pixel colour to red
                if (x >= 192 && x < 224)
                {
                    r = 0xFF;
                    g = 0x00;
                    b = 0x00;
                }

                // Set pixel colour to green
                if ((x >= 16 && x <= 72) || (x <= 16 && y >= 16 && y <= 134)) {
                    r = 0x00;
                    g = 0xFF;
                    b = 0x00;
                }
            }

            pixelData[index] = r;
            pixelData[index + 1] = g;
            pixelData[index + 2] = b;
            pixelData[index + 3] = 0xFF;
        }
    }

    gl.texSubImage2D(gl.TEXTURE_2D, 0, 0, 0, width, height, gl.RGBA, gl.UNSIGNED_BYTE, pixelData);

    console.log(totalBytes);
    console.log(pixelData.length);
    console.log(vram);
}

init();