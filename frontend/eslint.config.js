import js from "@eslint/js";
import nextPlugin from "@next/eslint-plugin-next";
import tsParser from "@typescript-eslint/parser";
import tsPlugin from "@typescript-eslint/eslint-plugin";
import prettierPlugin from "eslint-plugin-prettier";
import prettierConfig from "eslint-config-prettier";

/** @type {import("eslint").Linter.Config[]} */
export default [
    {
        ignores: ["node_modules", ".next", "dist", "next-env.d.ts"],
    },
    {
        files: ["**/*.{js,jsx,ts,tsx}"],
        languageOptions: {
            parser: tsParser,
            parserOptions: {
                ecmaVersion: "latest",
                sourceType: "module",
                ecmaFeatures: {
                    jsx: true,
                },
            },
            globals: {
                React: "readonly",
                JSX: "readonly",
                console: "readonly",
                setTimeout: "readonly",
                clearTimeout: "readonly",
                setInterval: "readonly",
                clearInterval: "readonly",
                window: "readonly",
                document: "readonly",
                navigator: "readonly",
                localStorage: "readonly",
                sessionStorage: "readonly",
                fetch: "readonly",
                process: "readonly",
                global: "readonly",
                Buffer: "readonly",
            },
        },
        plugins: {
            "@next/next": nextPlugin,
            "@typescript-eslint": tsPlugin,
            prettier: prettierPlugin,
        },
        rules: {
            ...js.configs.recommended.rules,
            ...tsPlugin.configs.recommended.rules,
            ...nextPlugin.configs["core-web-vitals"].rules,
            ...prettierConfig.rules,
            "prettier/prettier": [
                "error",
                {
                    semi: true,
                    singleQuote: false,
                    printWidth: 100,
                    tabWidth: 4,
                    trailingComma: "es5",
                    endOfLine: "auto",
                },
            ],
        },
    },
];
