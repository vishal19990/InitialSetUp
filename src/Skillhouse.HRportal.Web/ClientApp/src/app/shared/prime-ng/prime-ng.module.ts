import { NgModule } from "@angular/core";
import { TableModule } from "primeng/table";
import { DialogModule } from "primeng/dialog";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { InputTextareaModule } from "primeng/inputtextarea";
import { DropdownModule } from "primeng/dropdown";
import { RadioButtonModule } from "primeng/radiobutton";
import { CheckboxModule } from "primeng/checkbox";
import { CalendarModule } from 'primeng/calendar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputSwitchModule } from "primeng/inputswitch";
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ConfirmationService } from "primeng/api";
import { MessageService } from 'primeng/api';
import { MultiSelectModule } from 'primeng/multiselect';
import { ToastModule } from 'primeng/toast';
import { TabViewModule } from 'primeng/tabview';
import { CardModule } from 'primeng/card';
import {AutoCompleteModule} from 'primeng/autocomplete';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import {DataViewModule} from 'primeng/dataview';

@NgModule({
    declarations: [],
    imports: [
        TableModule,
        DialogModule,
        ButtonModule,
        InputTextModule,
        InputTextareaModule,
        CalendarModule,
        CheckboxModule,
        RadioButtonModule,
        DropdownModule,
        ConfirmDialogModule,
        InputSwitchModule,
        MultiSelectModule,
        ProgressSpinnerModule,
        ToastModule,
        TabViewModule,
        CardModule,MessagesModule,MessageModule,AutoCompleteModule,
        DataViewModule,
    ],
    exports: [
        TableModule,
        DialogModule,
        ButtonModule,
        InputTextModule,
        InputTextareaModule,
        CalendarModule,
        MultiSelectModule,
        CheckboxModule,
        RadioButtonModule,
        DropdownModule,
        ConfirmDialogModule,
        InputSwitchModule,
        ProgressSpinnerModule,
        ToastModule,
        TabViewModule,
        CardModule,MessagesModule,MessageModule,AutoCompleteModule,
        DataViewModule
    ],
    providers: [
        ConfirmationService,
        MessageService
    ],
})
export class PrimeNGModule { }
