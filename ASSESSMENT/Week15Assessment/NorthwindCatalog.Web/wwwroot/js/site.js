window.catalogUi = (function () {
    function initProductFilter(inputId, gridId, emptyStateId) {
        const input = document.getElementById(inputId);
        const grid = document.getElementById(gridId);
        const emptyState = document.getElementById(emptyStateId);

        if (!input || !grid) {
            return;
        }

        const cards = Array.from(grid.querySelectorAll('.product-item'));

        const applyFilter = () => {
            const term = (input.value || '').trim().toLowerCase();
            let visibleCount = 0;

            cards.forEach(card => {
                const name = card.getAttribute('data-name') || '';
                const matched = !term || name.includes(term);
                card.classList.toggle('d-none', !matched);
                if (matched) {
                    visibleCount++;
                }
            });

            if (emptyState) {
                emptyState.classList.toggle('d-none', visibleCount > 0);
            }
        };

        input.addEventListener('input', applyFilter);
    }

    return {
        initProductFilter
    };
})();
