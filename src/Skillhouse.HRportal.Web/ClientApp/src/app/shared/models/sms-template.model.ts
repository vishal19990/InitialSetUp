export class Lead {
  id?: number;
  mobileNo?: number;
}
export class LeadSmsTemplateModel {
  isPromotional: boolean;
  body?: string;
  templateId?: number;
  leadId?: number;
  templateType: string;
}
export class CampaignSmsTemplateModel {
  isPromotional: boolean;
  body?: string;
  templateId?: number;
  leadSetId?: number;
  templateType: string;
}

export class PersonalizationModel {
  id: number;
  name: string;
  placeHolder: string;
}
export class LeadSetModel {
  id: number;
  leadSetName: string;
}
