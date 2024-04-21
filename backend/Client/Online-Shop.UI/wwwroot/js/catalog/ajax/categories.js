document.addEventListener("DOMContentLoaded", async function () {
    let items = await fetchCatalogData();
    insertCatalogInfo(items);
    hasMorePages = hasMoreItems(items);
});

async function fetchCatalogData() {
    try {
        let url = `${apiOptions.baseUrl}items/list`

        
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
    for (let i = 0; i < rows.length; i++) {
        let rowHTML = `<div class="row">`
        let row = rows[i]
        for (let j = 0; j < row.length; j++) {
            let column = row[j]
            rowHTML += `<div class="column">
            <div class="card" onclick=buttonOnClick(${column.id})>
              <div class="card-img-container">
                  <img class="popular_img" src=${column.imageUrl} alt="�����������" />
              </div>             
              <div class="name"><p style="font-weight: 600">${column.name}</p></div>
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