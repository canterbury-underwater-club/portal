import type { VDataTable } from 'vuetify/components'

/*
The DataTableHeaders type is currently not exported (as of Vuetify 3.5.5).
The following types work around this, see https://stackoverflow.com/questions/75991355/import-datatableheader-typescript-type-of-vuetify3-v-data-table/75993081#75993081
*/
type ReadonlyHeaders = VDataTable['$props']['headers']
type UnwrapReadonlyArray<A> = A extends Readonly<Array<infer I>> ? I : never
export type ReadonlyDataTableHeader = UnwrapReadonlyArray<ReadonlyHeaders>
