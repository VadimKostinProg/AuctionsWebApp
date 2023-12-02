import { TableColumnSettingsModel } from "./tableColumnSettingsModel";

export class DataTableOptionsModel {
    title: string;
    showIndexColumn: boolean;
    showDeletedData: boolean;
    columnSettings: TableColumnSettingsModel[];
}