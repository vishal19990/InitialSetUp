export class EmailLeadModel{
    leadId?:number;
    templateId?:number;
    isPromotional:boolean;
    body?:string;
    subject?:string;
    preText?:string;
    templateType: string;

}

export class EmailCampaignModel{
    leadSetId?:number;
    templateId?:number;
    isPromotional:boolean;
    body?:string;
    subject?:string;
    preText?:string;
    templateType: string;
}