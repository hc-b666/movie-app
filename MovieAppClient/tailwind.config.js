/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      backgroundImage: {
        "auth-bg": "url('/auth-bg.jpg')",
      },
      container: {
        center: true,
        screens: {
          sm: "100%",
          md: "100%",
          lg: "720px",
          xl: "720px",
          "2xl": "1200px",
        },
      },
    },
  },
  plugins: [],
};
