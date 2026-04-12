(() => {
    // Root html element is used to switch theme variables in CSS.
    const root = document.documentElement;
    const toggle = document.getElementById("themeToggle");
    // Footer year is set dynamically so it stays current.
    const yearEl = document.getElementById("currentYear");
    const prefersLight = window.matchMedia("(prefers-color-scheme: light)").matches;

    // Load saved preference or fall back to OS/browser preference.
    const savedTheme = localStorage.getItem("devtrack-theme");
    const initialTheme = savedTheme ?? (prefersLight ? "light" : "dark");

    const applyTheme = (theme) => {
        root.setAttribute("data-theme", theme);
        root.classList.toggle("dark", theme === "dark");
    };

    applyTheme(initialTheme);

    if (toggle) {
        // Keep icon/aria text aligned with active theme for accessibility.
        const updateLabel = () => {
            const isLight = root.getAttribute("data-theme") === "light";
            toggle.querySelector(".icon-wrap").textContent = isLight ? "☀" : "◐";
            toggle.setAttribute("aria-label", isLight ? "Switch to dark mode" : "Switch to light mode");
        };

        updateLabel();

        toggle.addEventListener("click", () => {
            const next = root.getAttribute("data-theme") === "light" ? "dark" : "light";
            applyTheme(next);
            localStorage.setItem("devtrack-theme", next);
            updateLabel();
        });
    }

    // Keep copyright date fresh each year.
    if (yearEl) {
        yearEl.textContent = `${new Date().getFullYear()}`;
    }

    const menuToggle = document.getElementById("menuToggle");
    const mainNav = document.getElementById("mainNav");

    if (menuToggle && mainNav) {
        menuToggle.addEventListener("click", () => {
            const isOpen = mainNav.classList.toggle("open");
            menuToggle.setAttribute("aria-expanded", isOpen);
            menuToggle.setAttribute("aria-label", isOpen ? "Close navigation menu" : "Open navigation menu");
        });

        mainNav.addEventListener("click", (e) => {
            if (e.target.tagName === "A") {
                mainNav.classList.remove("open");
                menuToggle.setAttribute("aria-expanded", "false");
            }
        });
    }

    // Scroll-based reveal animation for dashboard cards.
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
        node.style.transitionDelay = `${index * 35}ms`;
        observer.observe(node);
    });
})();
