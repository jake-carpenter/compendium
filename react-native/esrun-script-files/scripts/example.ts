// Example script that will include top-level await
import { readFile } from 'fs/promises'

const fileContent = await readFile('./app.json')
const json = await JSON.parse(fileContent.toString())

console.log(json)
