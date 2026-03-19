document.addEventListener("DOMContentLoaded", function () {
  const root = document.documentElement;
  const toggleButton = document.getElementById("themeToggle");

  if (!toggleButton) {
    return;
  }

  const iconElement = toggleButton.querySelector(".theme-icon");
  const labelElement = toggleButton.querySelector(".theme-label");

  const setTheme = (theme) => {
    root.setAttribute("data-theme", theme);
    localStorage.setItem("cm-theme", theme);

    if (theme === "dark") {
      if (iconElement) {
        iconElement.textContent = "☀️";
      }
      if (labelElement) {
        labelElement.textContent = "Light";
      }
      toggleButton.setAttribute("aria-label", "Switch to light mode");
    } else {
      if (iconElement) {
        iconElement.textContent = "🌙";
      }
      if (labelElement) {
        labelElement.textContent = "Night";
      }
      toggleButton.setAttribute("aria-label", "Switch to night mode");
    }
  };

  const currentTheme =
    root.getAttribute("data-theme") === "dark" ? "dark" : "light";
  setTheme(currentTheme);

  toggleButton.addEventListener("click", function () {
    const nextTheme =
      root.getAttribute("data-theme") === "dark" ? "light" : "dark";
    setTheme(nextTheme);
  });
});
