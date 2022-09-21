import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SmsTemplateURLConstants } from "../constants/url-constants";
import {
  LeadSetModel,
  LeadSmsTemplateModel,
  PersonalizationModel,
} from "../models/sms-template.model";

@Injectable({ providedIn: "root" })
export class SmsTemplateService {
  constructor(private http: HttpClient) {}

  saveLeadSmsTemplate(smsTemplateModel: LeadSmsTemplateModel) {
    return this.http.post(
      SmsTemplateURLConstants.LEAD_SAVE_SMS,
      smsTemplateModel
    );
  }

  getLeadSets(): Observable<LeadSetModel[]> {
    return this.http.get<LeadSetModel[]>(SmsTemplateURLConstants.GET_LEAD_SETS);
  }

  getPersonalizations(): Observable<PersonalizationModel[]> {
    return this.http.get<PersonalizationModel[]>(
      SmsTemplateURLConstants.GET_PERSONALIZATIONS_URL
    );
  }
}
