import Products from "./Products";
import { getCatalogItems } from "../../api/catalog/getCatalogItems";
import { getBrands } from "../../api/catalog/getBrands";
import { getGenders } from "../../api/catalog/getGenders";
import { getSizes } from "../../api/catalog/getSizes";
import { getTypes } from "../../api/catalog/getTypes";
import {Filter} from "../../config/types/requests/filter";
import {parseFilterParam} from "../../utils/parseFilterParam";

type SearchParams = {
    searchParams: {
        page?: string;
        brand?: string | string[];
        type?: string | string[];
        gender?: string | string[];
        size?: string | string[];
    };
};

export default async function Items({ searchParams }: SearchParams) {
    const page = parseInt(searchParams.page || '0');

    const filters: Filter = {};

    const brandFilter = parseFilterParam(searchParams.brand);
    if (brandFilter) filters.BrandId = brandFilter;

    const typeFilter = parseFilterParam(searchParams.type);
    if (typeFilter) filters.TypeId = typeFilter;

    const genderFilter = parseFilterParam(searchParams.gender);
    if (genderFilter) filters.GenderId = genderFilter;

    const productsResponse = await getCatalogItems(page, filters);

    const brands = await getBrands();
    const genders = await getGenders();
    const types = await getTypes();
    const sizes = await getSizes();

    const filtersData = {
        brands,
        genders,
        types,
        sizes,
    };

    return <Products page={page} products={productsResponse?.data} productFilters={filtersData} />;
}
