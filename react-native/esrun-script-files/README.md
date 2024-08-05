# Using TypeScript script files that include top-level await

Top-level await is a nightmare in JavaScript. It's a mostly solved problem the the time of writing, but tooling isn't always on board.

## The problem

Create your TypeScript script as a `.ts` file in `./scripts`

```typescript
import { readFile } from 'fs/promises'

const fileContent = await readFile('./app.json')
const json = await JSON.parse(fileContent.toString())

console.log(json)
```

(also add the script in your `package.json`)

```json
{
  "scripts": {
    "example": "example ./scripts/example.ts"
  }
}
```

You should immediately notice that TypeScript is warning you that top-level await can only be used in special circumstances. However, it probably works just fine and TypeScript is just being annoying. You might be tempted to do what it says and start modifying your `tsconfig.json` file, but there are some caveats to be aware of.

## The solution

We'll need to to use `ESNext` as the target and module option. In the example `tsconfig.json` for this repo (and other Expo projects), it looks like this:

```json
{
  "extends": "expo/tsconfig.base",
  "compilerOptions": {
    "strict": true,
    "paths": {
      "@/*": [
        "./*"
      ]
    }
  },
  "include": [
    "**/*.ts",
    "**/*.tsx",
    ".expo/types/**/*.ts",
    "expo-env.d.ts"
  ],
}
```

However, the example in this repo extends a standard Expo `tsconfig.json` file that has _some_ of the defaults we need.

```json
{
  "$schema": "https://json.schemastore.org/tsconfig",
  "display": "Expo",

  "compilerOptions": {
    "allowJs": true,
    "esModuleInterop": true, // This is probably important
    "jsx": "react-native",
    "lib": ["DOM", "ESNext"],
    "moduleResolution": "node",
    "noEmit": true,
    "resolveJsonModule": true,
    "skipLibCheck": true,
    "target": "ESNext" // This is important
  },

  "exclude": ["node_modules", "babel.config.js", "metro.config.js", "jest.config.js"]
}
```

So, we also need to specify `"module": "ESNext"` in the `compilerOptions` object and can do so in our own `tsconfig.json`

```json
{
// ...
  "compilerOptions": {
    // ...
    "module": "ESNext"
  }
}
```

## What if it still doesn't work?

With all of that, we've told our TypeScript to behave correctly, but your editor may be using a different version of TypeScript. Be sure to switch it's configuration to use your workspace version.
