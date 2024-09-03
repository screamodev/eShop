import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/react';
import { ChevronDownIcon } from '@heroicons/react/20/solid';
import { useRouter, useSearchParams } from 'next/navigation';

type FiltersDropdownProps<T> = {
    filterName: string;
    dropdownItems: T[] | null;
    displayKey: keyof T; // Ключ для отображения значения
};

export default function FiltersDropdown<T extends { id: number }>({
                                                                      filterName,
                                                                      dropdownItems,
                                                                      displayKey
                                                                  }: FiltersDropdownProps<T>) {
    const router = useRouter();
    const searchParams = useSearchParams();

    const selectedFilterId = searchParams?.get(filterName);
    const selectedItem = dropdownItems?.find(item => item.id.toString() === selectedFilterId);

    const handleSelect = (id: number) => {
        const updatedParams = new URLSearchParams(searchParams?.toString());
        updatedParams.set(filterName, id.toString());
        router.push(`/?${updatedParams.toString()}`);
    };

    const handleClear = () => {
        const updatedParams = new URLSearchParams(searchParams?.toString());
        updatedParams.delete(filterName);
        router.push(`/?${updatedParams.toString()}`);
    };

    return (
        <Menu as="div" className="relative inline-block text-left">
            <MenuButton className="inline-flex w-40 justify-center gap-x-1.5 rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                {selectedItem ? (selectedItem[displayKey] as unknown as string) : `Select ${filterName}`}
                <ChevronDownIcon aria-hidden="true" className="ml-2 h-5 w-5 text-gray-400" />
            </MenuButton>

            <MenuItems
                className="absolute left-0 z-10 mt-2 w-56 origin-top-right divide-y divide-gray-100 rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5"
            >
                <div className="py-1">
                    {dropdownItems?.map(item => (
                        <MenuItem key={item.id}>
                            <button
                                onClick={() => handleSelect(item.id)}
                                className="block px-4 py-2 text-sm text-gray-700"
                            >
                                {item[displayKey] as unknown as string}
                            </button>
                        </MenuItem>
                    ))}
                </div>
                {selectedItem && (
                    <div className="py-1">
                        <MenuItem>
                            <button
                                onClick={handleClear}
                                className="block w-full px-4 py-2 text-sm text-left text-red-600"
                            >
                                Clear {filterName}
                            </button>
                        </MenuItem>
                    </div>
                )}
            </MenuItems>
        </Menu>
    );
}
