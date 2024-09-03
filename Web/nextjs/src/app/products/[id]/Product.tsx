"use client"
import React, { useState } from "react";
import Image from "next/image";
import { CatalogItem } from "../../../config/types/catalog/catalogItem";
import { useSession } from "next-auth/react";
import {useCart} from "../../../context/cart/CartContext";

type ProductProps = {
    product: CatalogItem
}

export default function Product({ product }: ProductProps) {
    const { data: session } = useSession();
    const { addItem } = useCart();

    const [showPopup, setShowPopup] = useState(false);

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

            setShowPopup(true);

            setTimeout(() => {
                setShowPopup(false);
            }, 3000);
        } catch (error) {
            console.error("Error adding item to cart:", error);
        }
    };

    return (
        <>
            <div
                className="content-container flex flex-col small:flex-row small:items-start py-6 relative"
                data-testid="product-container"
            >
                <div
                    className="flex flex-col small:sticky small:top-48 small:py-0 small:max-w-[300px] w-full py-8 gap-y-6">
                    <div id="product-info">
                        <div className="flex flex-col gap-y-4 lg:max-w-[500px] mx-auto">
                            <h2 className="text-3xl leading-10 text-ui-fg-base">
                                {product.name}
                            </h2>

                            <p className="text-medium text-ui-fg-subtle" data-testid="product-description">
                                {product.description}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="flex justify-center w-full relative">
                    <Image
                        src={product.pictureUrl}
                        alt="Thumbnail"
                        width={600}
                        height={600}
                    />
                </div>
                <div
                    className="flex flex-col small:sticky small:top-48 small:py-0 small:max-w-[300px] w-full py-8 gap-y-12">
                    <div className="flex justify-center gap-x-2">
                        <p className={"text-xl text-blue-400"}>
                            {"$" + product.price}
                        </p>
                    </div>
                    {!session?.accessToken && <p>Authorize to add product to the cart</p>}
                    <button
                        disabled={!session?.accessToken}
                        onClick={handleSubmit}
                        className="w-full h-10 text-center px-4 border hover:bg-neutral-200 disabled:opacity-20 transition-colors duration-300"
                    >
                        Add to cart
                    </button>
                </div>
            </div>

            {/* Popup */}
            {showPopup && (
                <div className="fixed bottom-4 right-4 bg-green-500 text-white p-4 rounded shadow-lg transition-transform transform">
                    <p>Product added to cart!</p>
                </div>
            )}
        </>
    );
}
