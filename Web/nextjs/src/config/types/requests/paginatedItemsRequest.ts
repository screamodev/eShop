
export type PaginatedItemsWithFiltersRequest<T extends string | number | symbol> = {
    pageIndex: number;
    pageSize: number;
    filters: Record<T, number[]>;
};