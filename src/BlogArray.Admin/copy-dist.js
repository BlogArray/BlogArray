const fs = require('fs-extra');
//const fs = require('fs');
//const { promises: fss } = require("fs")
const path = require('path');

const sourceDir = path.join(__dirname, 'dist', 'browser');
const destinationDir = path.join(__dirname, '..', 'BlogArray', 'bin', 'Release', 'net8.0', 'publish', 'wwwroot', 'admin');

fs.removeSync(destinationDir);
fs.copySync(sourceDir, destinationDir);

console.log(`Angular dist files copied to ${destinationDir}`);

