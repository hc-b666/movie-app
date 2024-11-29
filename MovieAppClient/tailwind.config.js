/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      backgroundImage: {
        "auth-bg": "url('/auth-bg.jpg')",
      },
    },
  },
  plugins: [],
};
