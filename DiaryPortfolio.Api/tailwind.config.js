/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Pages/**/*.cshtml", // If you ever use Razor Pages
        "./wwwroot/**/*.html",
        "./**/*.cshtml" // Direct catch-all for any CSHTML in the project
    ],
  theme: {
    extend: {},
  },
  plugins: [],
}

