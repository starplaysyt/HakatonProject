
document.addEventListener('DOMContentLoaded', () => {
    
    const cardsWrapper = document.getElementById('interest-cards-wrapper');
    if (!cardsWrapper) return;

    let currentExpandedCard = null;

    const INTERESTS_DATA = [
        {
            id: 'culture',
            title: 'Культура',
            color: 'var(--color-culture)',
            events: [
                { title: 'Вечер акустической музыки', description: 'Уютный вечер в формате квартирника...', time: '19:30 - 21:30', image: 'https://placehold.co/700x700/D9D0A8/333?text=Музыка' },
                { title: 'Экскурсия "Фонари улицы Гоголя"', description: 'Прогулка по знаменитой улице Гоголя...', time: '18:30 - 20:00', image: 'https://placehold.co/700x700/D9D0A8/333?text=Экскурсия' },
            ]
        },
        {
            id: 'sport',
            title: 'Спорт',
            color: 'var(--color-sport)',
            events: [
                { title: 'Велопробег "Мухавец-Тур"', description: 'Ежегодный осенний велопробег...', time: '09:00 - 13:00', image: 'https://placehold.co/700x700/CDBFC1/333?text=Велосипед' },
            ]
        },
        {
            id: 'volunteer',
            title: 'Волонтерство',
            color: 'var(--color-volunteer)',
            events: [
                { title: 'Акция "Чистый берег"', description: 'Сбор мусора и благоустройство...', time: '10:00 - 14:00', image: 'https://placehold.co/700x700/D4E1D1/333?text=Уборка' },
                { title: 'Сбор корма для приюта "Доброта"', description: 'Помощь в сортировке, упаковке...', time: '15:00 - 17:00', image: 'https://placehold.co/700x700/D4E1D1/333?text=Животные' },
            ]
        },
        {
            id: 'science',
            title: 'Наука',
            color: 'var(--color-science)',
            events: [
                { title: 'Лекция: "Искусственный интеллект"', description: 'Открытая лекция о трендах...', time: '18:00 - 20:00', image: 'https://placehold.co/700x700/BAD1D2/333?text=ИИ' },
            ]
        },
        {
            id: 'student',
            title: 'Студ. самоуправ.',
            color: 'var(--color-student)',
            events: [
                { title: 'Выборы председателя студсовета', description: 'Голосование за нового лидера...', time: '10:00 - 17:00', image: 'https://placehold.co/700x700/C0B4C8/333?text=Выборы' },
            ]
        },
    ];


    function generateEventCardHTML(event) {
        return `
            <div class="event-card">
                <div class="event-card-image" style="background-image: url('${event.image}')"></div>
                <div class="event-card-details">
                    <div>
                        <h2 class="event-title">${event.title}</h2>
                        <p class="event-description">${event.description}</p>
                    </div>
                    <div class="event-footer">
                        <span class="event-time">${event.time}</span>
                        <button class="btn-respond">Откликнуться</button>
                    </div>
                </div>
            </div>
        `;
    }
    

    function renderInterestCards() {
        cardsWrapper.innerHTML = INTERESTS_DATA.map(item => `
            <div class="interest-item-wrapper" data-id="${item.id}">
                <div class="interest-label">${item.title}</div>
                <div 
                    id="${item.id}"
                    class="interest-card"
                    style="background-color: ${item.color};"
                >
                    <div class="expanded-content">
                        <button class="close-button">&times;</button>
                        <h2 class="expanded-title">${item.title}</h2>
                        <div class="event-list-container">
                            </div>
                    </div>
                </div>
            </div>
        `).join('');
        
        document.querySelectorAll('.interest-card').forEach(card => {
            card.addEventListener('click', () => handleCardClick(card));
            
            card.querySelector('.close-button').addEventListener('click', (e) => {
                e.stopPropagation(); 
                closeExpandedView();
            });
        });
    }
    
    function handleCardClick(clickedCard) {
        if (clickedCard.classList.contains('is-expanded') || currentExpandedCard) {
            return;
        }
        runExpansion(clickedCard);
    }
    
    function runExpansion(clickedCard) {
        currentExpandedCard = clickedCard;
        const interestId = clickedCard.id;
        const wrapper = clickedCard.closest('.interest-item-wrapper');
        const interestData = INTERESTS_DATA.find(i => i.id === interestId);
        const eventsContainer = clickedCard.querySelector('.event-list-container');
        
        eventsContainer.innerHTML = interestData.events.map(generateEventCardHTML).join('');
        
        cardsWrapper.querySelectorAll('.interest-item-wrapper').forEach(w => {
            if (w !== wrapper) {
                w.classList.add('shrunk');
            }
        });
        
        wrapper.classList.add('is-expanded-parent');
        clickedCard.classList.add('is-expanded');
    }

    function closeExpandedView() {
        if (!currentExpandedCard) return;

        const card = currentExpandedCard;
        const wrapper = card.closest('.interest-item-wrapper');
        const eventsContainer = card.querySelector('.event-list-container');

        card.classList.remove('is-expanded');
        if (wrapper) {
            wrapper.classList.remove('is-expanded-parent'); 
        }
        
        cardsWrapper.querySelectorAll('.interest-item-wrapper').forEach(w => {
            w.classList.remove('shrunk');
        });
        
        currentExpandedCard = null;

        setTimeout(() => {
            eventsContainer.innerHTML = '';
        }, 600); 
    }
    
    // Инициализация
    renderInterestCards();
});