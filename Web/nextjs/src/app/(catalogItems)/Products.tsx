'use client'

import {useRouter, useSearchParams} from "next/navigation";
import { SlArrowLeft, SlArrowRight } from "react-icons/sl";
import {CatalogItem} from "../../config/types/catalog/catalogItem";
import FiltersDropdown from "../../components/filters/filtersDropdown";
import Product from "../../components/product";
import {CatalogFilters} from "../../config/types/catalog/catalogFilters";
import {CatalogBrand} from "../../config/types/catalog/catalogBrand";
import {CatalogType} from "../../config/types/catalog/catalogType";
import {CatalogGender} from "../../config/types/catalog/catalogGender";

type ProductsProps = {
    page: number;
    products?: CatalogItem[];
    productFilters: CatalogFilters
};

export default function Products({ products, page, productFilters }: ProductsProps) {
    const router = useRouter();
    const searchParams = useSearchParams();

    const handlePagination = (newPage: number) => {
        const params = new URLSearchParams(searchParams?.toString());
        params.set("page", newPage.toString());
        router.push(`/?${params.toString()}`);
    };

    return (
            <section className="p-10">
                <div className="flex">
                <div className="flex flex-col items-center w-1/4">
                    <div className="mb-4">
                        <p className="mb-2">Brand</p>
                        <FiltersDropdown<CatalogBrand> filterName="brand" dropdownItems={productFilters.brands} displayKey="brand" />
                    </div>

                    <div className="mb-4">
                        <p className="mb-2">Type</p>
                        <FiltersDropdown<CatalogType> filterName="type" dropdownItems={productFilters.types} displayKey="type" />

                    </div>

                    <div className="mb-4">
                        <p className="mb-2">Gender</p>
                        <FiltersDropdown<CatalogGender> filterName="gender" dropdownItems={productFilters.genders} displayKey="gender" />

                    </div>
                </div>
                <div className="w-full">
                    {products?.length ? <ul className="grid grid-cols-2 w-full small:grid-cols-3 medium:grid-cols-4 gap-x-6 gap-y-8"
                        data-testid="products-list">
                        {products.map((p) => (
                            <li key={p.id}>
                                <Product product={p}/>
                            </li>
                        ))}
                    </ul> : <div>Items not found.</div>}
                </div>
                </div>
                <div className="flex justify-center mt-8">
                        <button
                            className="p-4 mx-2 text-black rounded border disabled:opacity-15"
                            onClick={() => handlePagination(page - 1)}
                            disabled={page === 0}
                        >
                            <SlArrowLeft />
                        </button>
                        <button
                            className="p-4 mx-2 text-black rounded border disabled:opacity-15"
                            onClick={() => handlePagination(page + 1)}
                            disabled={products && (products.length < 8)}
                        >
                            <SlArrowRight />
                        </button>
                    </div>
            </section>
    );
}