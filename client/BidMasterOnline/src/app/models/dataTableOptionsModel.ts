import { TableColumnSettingsModel } from "./tableColumnSettingsModel";

export class DataTableOptionsModel {
    title: string;
    showIndexColumn: boolean;
    showDeletedData: boolean;
    allowCreating: boolean;
    showActionsColumn: boolean;
    actionNames: string[];
    columnSettings: TableColumnSettingsModel[];
}