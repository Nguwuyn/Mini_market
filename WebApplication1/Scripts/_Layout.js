﻿function markNav() {
    curr = location.pathname.split('/')[1];
    if (curr == "") {
        curr = "Home"
    }
    div = document.getElementById(`nav-${curr.toLowerCase()}`)
    if (div != null) {
        div.classList.add("selected")
    }
}

function postAddCart(id) {
    fetch("/ShoppingCart/AddToCart", {
        method: "POST",
        body: id
    }).then((response) => {
        if (!response.ok) {
            return console.error("wrongg!")
        }
        bs5Utils.Snack.show('secondary', 'Thêm vào giỏ hàng thành công!', 2500, true);
        const div = document.getElementById("cart_number");
        const StockQuantity = Number(div.innerHTML)
        div.style.display = "flex";
        div.innerHTML = StockQuantity + 1;
        return console.log("ok!")
    })
}

