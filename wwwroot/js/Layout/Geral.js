
// se for mobile
if (screen.width <= 480) {

    // icone
    var icon = document.querySelector('.icon');

    // menu com os items
    const menu = document.querySelector('.menu-links');

    // icon cadastro
    var icad = document.querySelector('#cadastros');

    // submenu cadastro
    const subcad = document.querySelector('#sub-cad');

    // icon cronogramas
    var icro = document.querySelector('#cronogramas');

    // submenu cronograma
    const subcro = document.querySelector('#sub-cro');


    // quando clicar no icone hamburguer, se os items estiverem escondidos, aparecem, e vice-versa
    icon.addEventListener('click', function () {
        if (menu.style.display === "block") {
            menu.style.display = "none";
        } else {
            menu.style.display = "block";
        } 
    });

    // quando clicar, se o submenu estiver escondido aparece, e vice-versa
    icad.addEventListener('click', function () {
        if (subcad.style.display === "block") {
            subcad.style.display = "none";
        } else {
            subcad.style.display = "block";
        }
    });

    // quando clicar, se o submenu estiver escondido aparece, e vice-versa
    icro.addEventListener('click', function () {
        if (subcro.style.display === "block") {
            subcro.style.display = "none";
        } else {
            subcro.style.display = "block";
        }
    });

    
}

else {
    const menuMobile = document.querySelector('.menu-mobile-header');
    menuMobile.style.display = "none";
}




