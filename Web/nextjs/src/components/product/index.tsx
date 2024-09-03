import {CatalogItem} from "../../config/types/catalog/catalogItem";
import Link from "next/link";
import Image from 'next/image'
import React, {useState} from "react";
import {useSession} from "next-auth/react";
import {useCart} from "../../context/cart/CartContext";

type ProductProps = {
    product: CatalogItem;
};

export default function Product({ product }: ProductProps) {
    const { data: session } = useSession();
    const { addItem } = useCart();

    const handleSubmit = async () => {
        try {
            addItem({
                id: product.id,
                name: product.name,
                price: product.price,
                pictureUrl: product.pictureUrl,
                catalogItemId: product.id,
                gender: "product",
                size: "product",
                count: 1,
            })

        } catch (error) {
            console.error("Error adding item to cart:", error);
        }
    };

    return (
        <>
            <Link
                href={`/products/${product.id}`}
                className="group"
            >
                <div>
                    <div className={
                        "relative w-full overflow-hidden p-4 my-2 bg-neutral-100 shadow-elevation-card-rest rounded-large group-hover:shadow-elevation-card-hover transition-shadow ease-in-out duration-150"}>
                        <Image
                            src={product.pictureUrl}
                            alt="Thumbnail"
                            width={500}
                            height={500}
                        />
                    </div>
                    <div className="flex my-4 txt-compact-medium mt-4 justify-between">
                        <p className="text-sm" data-testid="product-title">{product.name}</p>
                        <div className="flex items-center gap-x-2">
                            <p
                                className={"text-sm text-blue-400"}
                            >
                                {"$" + product.price}
                            </p>
                        </div>
                    </div>
                </div>
            </Link>
            <button
                disabled={!session?.accessToken}
                onClick={handleSubmit}
                className="my-2 p-2 w-full txt-compact-medium text-center px-2 border hover:bg-neutral-200 disabled:opacity-20 transition-colors duration-300"
            >
                Add to cart
            </button>
        </>
    )
}
