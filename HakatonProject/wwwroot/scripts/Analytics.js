// Данные для диаграммы: процент созданных мероприятий (Условные данные)
const labels = ['Культура', 'Спорт', 'Волонтерство', 'Наука', 'Студ. самоуправ.'];
const percentageData = [25, 18, 40, 20, 12]; // Процент использования потенциала
const barColors = [
    '#a3ad6a', // Культура
    '#c9797f', // Спорт
    '#79c98a', // Волонтерство
    '#79a8c9', // Наука
    '#9c79c9'  // Студ. самоуправ.
];

const chartData = {
    labels: labels,
    datasets: [{
        data: percentageData,
        backgroundColor: barColors,
        borderRadius: 12,
        maxBarThickness: 150, // Ширина столбцов
    }]
};

// Инициализация Chart.js
const chart = new Chart(document.getElementById('barChart'), {
    type: 'bar',
    data: chartData,
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: { display: false },
            title: {
                display: true,
                text: 'Процент созданных мероприятий по тематикам (от 100%)',
                color: 'rgba(255, 255, 255, 0.8)',
                font: { size: 16, family: "'Nunito', sans-serif" }
            },
            tooltip: {
                backgroundColor: '#2D2F3E', // Наш фон
                bodyColor: '#E0E0E0', // Наш текст
                titleFont: { family: "'Nunito', sans-serif" },
                bodyFont: { family: "'Nunito', sans-serif" },
                callbacks: {
                    label: function(context) {
                        return context.label + ': ' + context.parsed.y + '%';
                    },
                    title: function() { return ''; } 
                }
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                max: 100,
                grid: { 
                    color: 'rgba(255, 255, 255, 0.1)',
                    drawBorder: false
                },
                ticks: {
                    color: 'rgba(255, 255, 255, 0.7)',
                    font: { family: "'Nunito', sans-serif" },
                    callback: function(value) { return value + "%"; }
                },
                border: { display: false }
            },
            x: {
                grid: { display: false },
                ticks: { 
                    color: 'rgba(255, 255, 255, 0.7)',
                    font: { size: 14, family: "'Nunito', sans-serif" }
                },
                border: { display: false }
            }
        }
    }
});