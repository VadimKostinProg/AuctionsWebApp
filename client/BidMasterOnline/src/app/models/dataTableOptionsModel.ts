import { FormOptionsModel } from "./formOptionsModel";
import { TableColumnSettingsModel } from "./tableColumnSettingsModel";

export class DataTableOptionsModel {
    public title: string;
    public resourceName: string;
    public showIndexColumn: boolean;
    public showDeletedData: boolean;
    public allowCreating: boolean;
    public createFormOptions: FormOptionsModel;
    public allowEdit: boolean;
    public editFormOptions: FormOptionsModel;
    public allowDelete: boolean;
    public emptyListDisplayLabel: string;
    public columnSettings: TableColumnSettingsModel[];
}