// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

import { dotnet } from './dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

setModuleImports('main.js', {
    window: {
        location: {
            href: () => globalThis.window.location.href
        }
    }
});

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);

await dotnet.run();

function start() {
    Methods.StartGame();
}

const startButton = document.querySelector('#startButton');
startButton.addEventListener('click', start, false);

window.addEventListener("keydown", (event) => {
    Methods.SetKey(event.keyCode, true);
});

window.addEventListener("keyup", (event) => {
    Methods.SetKey(event.keyCode, false);
});

export const Methods = exports.Program;
