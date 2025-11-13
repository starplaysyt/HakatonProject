// Ждем, пока страница загрузится
document.addEventListener('DOMContentLoaded', () => {

    // Получаем наш "холст"
    const ctx = document.getElementById('weekStatsChart').getContext('2d');

    // --- ДАННЫЕ ДЛЯ ГРАФИКА ---
    // (Примерно "снял" с вашей картинки)
    // Для "плавающих" баров мы передаем массив [начало, конец]
    const data = {
        labels: ['ПН', 'ВТ', 'СР', 'ЧТ', 'ПТ', 'СБ', 'ВС'],
        datasets: [{
            label: 'Активность',
            // Вот магия "плавающих" баров: [y_start, y_end]
            data: [
                [18, 19.5],    // ПН (Примерно 18:00 - 19:30)
                [13, 15],     // ВТ
                [16.5, 18.5], // СР
                [10, 13],     // ЧТ
                [14, 17],     // ПТ
                [10.5, 15],   // СБ
                null          // ВС (нет данных)
            ],
            // Цвета для каждого столбца
            backgroundColor: [
                '#82D1B3', // ПН
                '#D9B884', // ВТ
                '#D19282', // СР
                '#B38FDB', // ЧТ
                '#82D19A', // ПТ
                '#D7D789', // СБ
                'transparent' // ВС
            ],
            // Вот магия закругленных углов
            borderRadius: 8,
            borderSkipped: false, // Применяем ко всем углам
        }]
    };

    // --- НАСТРОЙКИ ГРАФИКА (ОСИ, ЛЕГЕНДА) ---
    const options = {
        responsive: true,
        maintainAspectRatio: false, // Позволяет графику заполнить контейнер

        // Отключаем легенду (кружок с надписью "Активность")
        plugins: {
            legend: {
                display: false
            },
            tooltip: {
                enabled: false // Отключаем подсказки при наведении
            }
        },

        // Настройка осей
        scales: {
            // Ось X (Дни недели)
            x: {
                position: 'top', // Метки 'ПН', 'ВТ' будут сверху
                grid: {
                    display: false // Убираем вертикальные линии сетки
                },
                border: {
                    display: false // Убираем саму ось X
                },
                ticks: {
                    color: '#E0E0E0' // Цвет текста меток
                }
            },
            // Ось Y (Время)
            y: {
                min: 10,  // Начинаем с 10:00
                max: 20,  // Заканчиваем в 20:00
                grid: {
                    color: '#4A4C60', // Цвет горизонтальных линий сетки
                    drawTicks: false // Убираем "засечки" на оси
                },
                border: {
                    display: false // Убираем саму ось Y
                },
                ticks: {
                    color: '#E0E0E0', // Цвет текста (10:00, 12:00...)
                    stepSize: 2, // Шаг в 2 часа
                    // Форматируем метки, чтобы было "10:00", а не "10"
                    callback: function (value, index, ticks) {
                        return value + ':00';
                    },
                    padding: 10 // Отступ меток от края
                }
            }
        }
    };

    // --- СОЗДАЕМ ГРАФИК ---
    new Chart(ctx, {
        type: 'bar', // Тип графика - столбчатый
        data: data,
        options: options
    });
});