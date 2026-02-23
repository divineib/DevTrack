(() => {
    const root = document.documentElement;
    const toggle = document.getElementById("themeToggle");
    const yearEl = document.getElementById("currentYear");
    const prefersLight = window.matchMedia("(prefers-color-scheme: light)").matches;

    const savedTheme = localStorage.getItem("devtrack-theme");
    const initialTheme = savedTheme ?? (prefersLight ? "light" : "dark");
    root.setAttribute("data-theme", initialTheme);

    if (toggle) {
        const updateLabel = () => {
            const isLight = root.getAttribute("data-theme") === "light";
            toggle.querySelector(".icon-wrap").textContent = isLight ? "☀" : "◐";
            toggle.setAttribute("aria-label", isLight ? "Switch to dark mode" : "Switch to light mode");
        };

        updateLabel();

        toggle.addEventListener("click", () => {
            const next = root.getAttribute("data-theme") === "light" ? "dark" : "light";
            root.setAttribute("data-theme", next);
            localStorage.setItem("devtrack-theme", next);
            updateLabel();
        });
    }

    if (yearEl) {
        yearEl.textContent = `${new Date().getFullYear()}`;
    }

    const animated = document.querySelectorAll(".fade-in");
    const observer = new IntersectionObserver(
        (entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("visible");
                    observer.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.15 }
    );

    animated.forEach((node, index) => {
        node.style.transitionDelay = `${index * 70}ms`;
        observer.observe(node);
    });
})();
