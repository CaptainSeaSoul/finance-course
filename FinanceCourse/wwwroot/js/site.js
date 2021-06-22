// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Literally Canvas
var lc;
var dotnetHelper;
var jsonPainting;

var observer = new IntersectionObserver(function (entries) {
    if (entries[0].isIntersecting === true) {
        lc = LC.init(
            document.getElementsByClassName('painter-component-widget')[0],
            { imageURLPrefix: '/lib/literally-canvas/img' }
        );

        if (jsonPainting != null)
            lc.loadSnapshot(JSON.parse(jsonPainting));

        lc.on('drawingChange', function () {
            dotnetHelper.invokeMethodAsync('FinanceCourse',
                JSON.stringify(lc.getSnapshot()));
        });
    }

}, { threshold: [0] });


function LCinitOnVisible(_dotnetHelper, _jsonPainting) {
    dotnetHelper = _dotnetHelper;
    jsonPainting = _jsonPainting;
    observer.observe(document.querySelector(".painter-component-widget"));
}

function LCloadJson(snapshotJSON) {
    lc.loadSnapshot(JSON.parse(snapshotJSON));
}

function LCgetData64Base() {
    return lc.getImage().toDataURL();
}


// chart.js

// life wheel chart
var lifeWheelChart;

function drawLifeWheelChart(_labels, _data) {
    var ctx = document.getElementById('lifeWheelChart');

    lifeWheelChart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: _labels,
            datasets: [{
                label: 'Your Life Stats',
                backgroundColor: 'rgb(0, 255, 255, 0.3)',
                data: _data,
                clip: 10
            }]
        },
        options: {
            scales: {
                r: {
                    suggestedMin: 0,
                    suggestedMax: 10
                }
            }
        }
    });
}

function getLifeWheelDrawing64Base() {
    return lifeWheelChart.toBase64Image("image/jpeg", 0.7);
}

function updateLifeWheelData(_labels, _data) {
    if (_labels != null)
        lifeWheelChart.data.labels = _labels;

    if (_data != null)
        lifeWheelChart.data.datasets[0].data = _data;

    lifeWheelChart.update();
}

// net worth graph
var netWorthGraph;

function drawNetWorthGraph(_data) {
    var ctx = document.getElementById('NetWorthGraph');

    netWorthGraph = new Chart(ctx, {
        type: 'line',
        data: {
            labels: Array.from({ length: 30 }, (_, i) => i + 1),
            datasets: [{
                label: 'Your Net Worth Over The Last 30 Days',
                backgroundColor: 'rgb(0, 255, 255, 0.3)',
                data: _data,
            }]
        }
    });
}

function updateNetWorthGraphData(_data) {
    if (_data != null && netWorthGraph !== undefined) {
        netWorthGraph.data.datasets[0].data = _data;

        netWorthGraph.update();
    }
}