import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EmailTemplateURLConstants } from "../constants/url-constants";
import { PersonalizationModel } from "../models/sms-template.model";
import { EmailLeadModel } from "../models/templateModel";
import { EmailCampaignModel } from "../models/templateModel";

@Injectable({ providedIn: "root" })
export class EmailTemplateService {
  constructor(private http: HttpClient) {}

  saveLeadEmailTemplate(templateModel: EmailLeadModel) {
    return this.http.post(EmailTemplateURLConstants.SAVE_LEAD_EMAIL_TEMPLATE, templateModel);
  }
  

  saveCampaignEmailTemplate(templateModel: EmailCampaignModel) {
    return this.http.post(EmailTemplateURLConstants.SAVE_CAMPAIGN_EMAIL_TEMPLATE, templateModel);
  }

  getPersonalizations(): Observable<PersonalizationModel[]> {
    return this.http.get<PersonalizationModel[]>(
      EmailTemplateURLConstants.GET_PERSONALIZATIONS_URL
    );
  }
}
