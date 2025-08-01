document.getElementById('btnPng').addEventListener('click', function () {
    chart.exportChart({ type: 'image/png' });
});

document.getElementById('btnSvg').addEventListener('click', function () {
    chart.exportChart({ type: 'image/svg+xml' });
});