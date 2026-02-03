interface Product{
    id: string;
    title: string;
    price: number;
}

function displayProduct(product : Product){
    console.log(`id: ${product.id}, title: ${product.title}, price: ${product.price}`)
}

const apiData = { id: "123", title: "Teclado", price: 450, manufacturer: "TechGear", stock: 15 }


displayProduct(apiData);

