document.addEventListener('DOMContentLoaded', () => {
  const toggle = document.querySelector('.nav-toggle');
  const nav = document.querySelector('.main-nav');
  toggle?.addEventListener('click', () => { const open = nav.classList.toggle('open'); toggle.setAttribute('aria-expanded', open); });
  document.querySelectorAll('.favorite-form').forEach(form => form.addEventListener('submit', async e => {
    if (!window.fetch) return;
    e.preventDefault();
    const button = form.querySelector('button');
    const response = await fetch(form.action, { method:'POST', body:new FormData(form), headers:{'X-Requested-With':'XMLHttpRequest'} });
    if (!response.ok) { form.submit(); return; }
    const data = await response.json();
    button.textContent = data.isFavorite ? '♥' : '♡';
    button.setAttribute('aria-label', data.message);
    showToast(data.message);
  }));
});
function showToast(message){ let toast=document.querySelector('.toast'); if(!toast){toast=document.createElement('div');toast.className='toast';document.body.appendChild(toast)} toast.textContent=message;toast.classList.add('show');setTimeout(()=>toast.classList.remove('show'),2200); }
