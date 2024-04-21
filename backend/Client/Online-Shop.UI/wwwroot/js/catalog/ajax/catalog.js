var page = 1;
var hasMorePages = true;
const size = 6;
const productList = document.getElementById('catalogItems');
const loader = document.querySelector('.loader');
var processingStarted = false;

const searchBar = document.getElementById(`query`);
const button = document.getElementById(`searchButton`);
searchBar.addEventListener('search', async () => {
    processingStarted = true;
    page = 1;
    hasMorePages = true;
    let items = await fetchCatalogData(page, size);
    insertCatalogInfo(items, false);
    processingStarted = false;
});

button.addEventListener('click', async () => {
    processingStarted = true;
    page = 1;
    hasMorePages = true;
    let items = await fetchCatalogData(page, size);
    insertCatalogInfo(items, false);
    processingStarted = false;
});

document.addEventListener("DOMContentLoaded", async function () {
    processingStarted = true;
    let items = await fetchCatalogData();
    insertCatalogInfo(items);
    hasMorePages = hasMoreItems(items);
    processingStarted = false;
});

document.addEventListener("DOMContentLoaded", async function () {
    let url = `${apiOptions.baseUrl}items/categories/list`
    let categories = await (await fetch(url)).json();
    let innerHtml = ``;
    categories.forEach((category) => {
        innerHtml += `<label style="align-items:center; display: block;">
            <input type="checkbox" name="select" value="${category.id}" />
            ${category.name}
        </label>`
    });
    let form = this.getElementById("categories");
    form.innerHTML = innerHtml;
});

async function fetchCatalogData() {
    let searchString = document.getElementById("query").value;
    let minPrice = parseInt(document.getElementById("min-price").value);
    let maxPrice = parseInt(document.getElementById("max-price").value);
    let categoriesForm = document.getElementById("categories");
    let categoriesBoxes = categoriesForm.querySelectorAll('input[type=checkbox]:checked');
    let categoriesIds = [];
    categoriesBoxes.forEach(x => categoriesIds.push(parseInt(x.value)));
    try {
        let url = `${apiOptions.baseUrl}items/list?page=${page}&size=${size}`
        
        if(searchString != null && searchString.length != 0){
            url += `&search=${searchString}`
        }
        if (!Number.isNaN(minPrice)) {
            url += `&minPrice=${minPrice}`
        }
        if (!Number.isNaN(maxPrice)) {
            url += `&maxPrice=${maxPrice}`
        }
        categoriesIds.forEach((x) => {
            url += `&categoriesIds=${x}`
        });
        const response = await fetch(url);
        
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        let jsonResponse = await response.json();
        page = jsonResponse.paginationContext.page + 1;
        return jsonResponse;
    } catch (error) {
        console.error('Error fetching catalog data:', error);
    }
}

function insertCatalogInfo(catalogData, isUpdate) {
    const productsList = document.getElementById("catalogItems")
    
    const chunkSize = 3;
    let rows = [];
    for (let i = 0; i < catalogData.items.length; i += chunkSize) {
        let chunk = catalogData.items.slice(i, i + chunkSize);
        rows.push(chunk)
    }
    
    let innerHTML = ""
    for(let i = 0; i < rows.length; i++){
        let rowHTML = `<div class="row">`
        let row = rows[i]
        for(let j = 0; j < row.length; j++){
            let column = row[j]
            rowHTML += `<div class="column">
            <div class="card" onclick=buttonOnClick(${column.id})>
              <div class="card-img-container">
                  <img class="popular_img" src=${column.imageUrl} alt="Изображение" />
              </div>             
              <div class="name"><p style="font-weight: 600">${column.name}</p></div>
              <div class="price"><p>${column.price} P</p></div>
              <button class="popular_btn" onclick=buttonOnClick(${column.id})>
                Подробнее
              </button>
            </div>
          </div>`
        }
        rowHTML += `</div>`
        innerHTML += rowHTML
    }
    if (isUpdate) {
        productsList.innerHTML += innerHTML;
    }
    else {
        productsList.innerHTML = innerHTML;
    }
    
}



async function buttonOnClick(id) {
    window.location.href = `/catalog/${id}/details`
};

const hideLoader = () => {
    loader.classList.remove('show');
};
const showLoader = () => {
    loader.classList.add('show');
};

const hasMoreItems = (items) => {
    return items.paginationContext.totalPages >= page
};

const loadPages = async () => {
    // show the loader 
    while (processingStarted) {
        await (new Promise(resolve => setTimeout(resolve, 100)));
    }
    processingStarted = true;
    showLoader();
    try {
        // if having more facts to fetch 
        if (hasMorePages) {
            processingStarted = true;
            // call the API to get facts 
            let response = await fetchCatalogData();

            hasMorePages = hasMoreItems(response);
            // show facts 
            insertCatalogInfo(response, true);
        }
    } catch (error) {
        console.log(error.message);
    } finally {
        if (!hasMorePages) {
            hideLoader();
        }
        processingStarted = false;
    }
};

var timer;
window.addEventListener('scroll', () => {
    const {
        scrollTop,
        scrollHeight,
        clientHeight
    } = document.documentElement;

    timer && clearTimeout(timer);
    if (scrollTop + clientHeight >= scrollHeight - 300 &&
        hasMoreItems) {
        timer = setTimeout(function () {
            loadPages();
        }, 300);
    }
}, {
    passive: false
});