export class Product {
    id!: number;
    productName!: string;
    barcodeNumber!: number;
    price!: number;
    description!: string;
    quantity!: number;
    productImages!: productImage[];
}

export class productImage{
    id!: number;
    productId!: number;
    imagePath!: string;
}
