/* modal */
const openModal = document.querySelector('.btn-modal');
const closeModal = document.querySelector('.close-modal');
const modal = document.querySelector('.modal-help');
const content = document.querySelector('.help-content');

openModal.addEventListener('click', function () {
    modal.style.display = 'flex';
    setTimeout(function () {
        content.classList.add('modal-active');
    }, 300);
});

closeModal.addEventListener('click', function () {
    content.classList.remove('modal-active');

    setTimeout(function () {
        modal.style.display = 'none';
    }, 600);
    
});