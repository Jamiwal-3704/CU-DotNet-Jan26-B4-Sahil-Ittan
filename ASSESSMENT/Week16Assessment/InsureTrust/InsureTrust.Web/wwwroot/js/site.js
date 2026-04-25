/* ============================================================
   INSURE TRUST — SITE JS
   Covers: theme toggle, flash dismiss, icon picker,
           purchase form, claim docs, premium calculator,
           modal helpers.
   ============================================================ */

/* ───────────────────────────────────────────────
   1. THEME — must run BEFORE DOMContentLoaded
      so there is no flash of wrong theme
   ─────────────────────────────────────────────── */
(function () {
    const saved = localStorage.getItem('nb-theme') || 'light';
    document.documentElement.setAttribute('data-theme', saved);
})();

/* ───────────────────────────────────────────────
   2. DOM READY
   ─────────────────────────────────────────────── */
document.addEventListener('DOMContentLoaded', function () {

    /* --- 2a. Theme toggle ----------------------------------- */
    const btn = document.getElementById('themeToggle');
    if (btn) {
        // Make sure the current saved theme is already applied
        const currentTheme = document.documentElement.getAttribute('data-theme') || 'light';
        localStorage.setItem('nb-theme', currentTheme);

        btn.addEventListener('click', function (e) {
            const html = document.documentElement;
            const next = html.getAttribute('data-theme') === 'dark' ? 'light' : 'dark';

            // --- Ripple effect ---
            const ripple = document.createElement('span');
            ripple.className = 'ripple';
            // position ripple at mouse coords inside button
            const rect = btn.getBoundingClientRect();
            const x = e.clientX - rect.left - 10;
            const y = e.clientY - rect.top  - 10;
            ripple.style.left = x + 'px';
            ripple.style.top  = y + 'px';
            btn.appendChild(ripple);
            setTimeout(() => ripple.remove(), 520);

            // --- Flip the logo / icons with a bounce ---
            const shield = document.querySelector('.brand-shield');
            if (shield) {
                shield.style.transform = 'rotate(180deg) scale(1.4)';
                shield.style.color     = next === 'dark' ? '#FFD60A' : '#FFD60A';
                setTimeout(() => {
                    shield.style.transform = '';
                }, 420);
            }

            // --- Apply new theme ---
            html.setAttribute('data-theme', next);
            localStorage.setItem('nb-theme', next);

            // Update button border color for non-navbar version
            btn.style.borderColor = next === 'dark'
                ? 'rgba(240,240,232,0.3)'
                : 'rgba(255,255,255,0.25)';
        });
    }

    /* --- 2b. Auto-dismiss flash messages ------------------- */
    const flashes = document.querySelectorAll('.flash');
    flashes.forEach(f => {
        setTimeout(() => {
            f.style.opacity    = '0';
            f.style.transition = 'opacity .5s';
            setTimeout(() => f.remove(), 500);
        }, 4000);
    });

    /* --- 2c. Icon picker ------------------------------------ */
    document.querySelectorAll('.icon-option').forEach(opt => {
        opt.addEventListener('click', function () {
            document.querySelectorAll('.icon-option').forEach(o => o.classList.remove('selected'));
            this.classList.add('selected');
            const input = document.getElementById('iconInput');
            if (input) input.value = this.dataset.icon;
        });
    });

    /* --- 2d. Dynamic field collection on purchase form ------ */
    const purchaseForm = document.getElementById('purchaseForm');
    if (purchaseForm) {
        purchaseForm.addEventListener('submit', function () {
            const fields = {};
            document.querySelectorAll('[data-dynamic-field]').forEach(inp => {
                fields[inp.name] = inp.value;
            });
            document.getElementById('dynamicFieldsInput').value = JSON.stringify(fields);
        });
    }

    /* --- 2e. Claim doc upload tracking --------------------- */
    document.querySelectorAll('.doc-upload-input').forEach(input => {
        input.addEventListener('change', function () {
            const item = this.closest('.claim-doc-item');
            if (item && this.files.length > 0) {
                item.classList.add('uploaded');
                item.querySelector('.doc-status').textContent = '✓ ' + this.files[0].name;
            }
            updateDocCount();
        });
    });

    /* --- 2f. Premium calculator ----------------------------- */
    const calcForm = document.getElementById('calcForm');
    if (calcForm) {
        ['calc-type', 'calc-age', 'calc-cover', 'calc-tenure'].forEach(id => {
            const el = document.getElementById(id);
            if (el) el.addEventListener('input', calcEstimate);
        });
        calcEstimate();
    }
});

/* ───────────────────────────────────────────────
   3. HELPER FUNCTIONS
   ─────────────────────────────────────────────── */

function updateDocCount() {
    const total    = document.querySelectorAll('.doc-upload-input').length;
    const uploaded = document.querySelectorAll('.claim-doc-item.uploaded').length;
    const counter  = document.getElementById('docCounter');
    if (counter) {
        counter.textContent  = uploaded + ' of ' + total + ' documents uploaded';
        counter.style.background = uploaded === total && total > 0 ? '#007730' : '#0a0a0a';
        counter.style.color      = '#FFD60A';
    }
}

function calcEstimate() {
    const type   = document.getElementById('calc-type')?.value   || 'Term Life';
    const age    = parseInt(document.getElementById('calc-age')?.value)    || 30;
    const cover  = parseFloat(document.getElementById('calc-cover')?.value) || 1000000;
    const tenure = parseInt(document.getElementById('calc-tenure')?.value)  || 24;

    const rates = {
        'Term Life': 0.010, 'Health': 0.008, 'Vehicle': 0.015, 'Home': 0.005,
        'Property': 0.012, 'Employee Group Benefits': 0.018, 'Engineering': 0.014
    };
    const rate      = rates[type] || 0.01;
    const ageFactor = 1 + (age - 30) * 0.02;
    const monthly   = Math.round(cover * rate * ageFactor / 12);
    const total     = monthly * tenure;

    const monthlyEl = document.getElementById('calc-monthly');
    const totalEl   = document.getElementById('calc-total');
    if (monthlyEl) monthlyEl.textContent = '₹' + monthly.toLocaleString('en-IN');
    if (totalEl)   totalEl.textContent   = '₹' + total.toLocaleString('en-IN');
}

function confirmDelete(formId, msg) {
    if (confirm(msg || 'Are you sure?')) {
        document.getElementById(formId).submit();
    }
}

function toggleEditModal(policyId, tenure, pkg) {
    const modal = document.getElementById('editModal');
    if (modal) {
        document.getElementById('editPolicyId').value = policyId;
        document.getElementById('editTenure').value   = tenure;
        document.getElementById('editPackage').value  = pkg;
        modal.style.display = modal.style.display === 'flex' ? 'none' : 'flex';
    }
}

function closeModal(id) {
    const modal = document.getElementById(id);
    if (modal) modal.style.display = 'none';
}
