import {notFound} from "next/navigation";
import {getCatalogItemById} from "../../../api/catalog/getCatalogItemById";
import Product from "./Product";

type ParamsProps = {
    params: { id: string }
}

export default async function ProductTemplate({ params }: ParamsProps) {
    const product = await getCatalogItemById(+params.id)

    if (!product) {
        notFound()
    }

    return (
        <Product product={product} />
    )
}