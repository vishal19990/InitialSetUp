import { environment } from "../../../environments/environment";

const apiUrl = environment.apiUrl;

export class LoginURLConstants {
    static LOGIN = apiUrl + "/api/user";
}
export class RoleManagementURLConstants {
    static SAVEROLEPERMISSION = apiUrl + "/api/role/saveRolePermission";
    static GETPERMISSIONS = apiUrl + "/api/role/GetAllPermission";
    static GET_ROLE = apiUrl + "/api/role/GetAllRoles";
    static GET_PERMISSION_BY_ROLE_ID = apiUrl + "/api/role/";
    static DELETEROLEPERMISSION = apiUrl + "/api/role/";
    static GET_ALL_ROLES = apiUrl + "/api/role/GetRoles";
}
export class AppUrlConstants {
    static GET_NAV_PERMISSIONS_URL = apiUrl + "/api/Common/GetNavPermissions";
}

export class UserURLConstants {
    static GET_USERS_URL = apiUrl + "/api/user/GetAllUser";
    static GET_USER_BY_ID_URL = apiUrl + "/api/user/GetUserById";
    static ADDUSER = apiUrl + "/api/user";
    static GETROLES = apiUrl + "/api/role";
    static UPDATEUSER = apiUrl + "/api/user/updateUser";
    static GETLANGUAGES = apiUrl + "/api/user";
    static ADDLANGUAGE = apiUrl + "/api/user";
    static DELETELANGUAGE = apiUrl + "/api/user";
    static DELETE_USER = apiUrl + "/api/user/";
    static RESET_PASSWORD = apiUrl + "/api/user/ChangePassWord";
    static GET_USERID = apiUrl + "/api/user/GetAllUserId";
}

export class GetAllRolesURLConstants {
    static GET_ALL_ROLES = apiUrl + "/api/role/GetAllRoles";
}

export class CourseURLConstants {
    static SAVE_COURSE = apiUrl + "/api/course/SaveCourse";
    static DELETE_COURSE = apiUrl + "/api/course/";
    static GET_ALL_COURSES = apiUrl + "/api/course/GetAllCourses";
    static GET_ALL_DEPARTMENTS = apiUrl + "/api/course/GetAllDepartments";
    static GET_COURSE_BY_ID = apiUrl + "/api/course/";
    static UPDATE_COURSE = apiUrl + "/api/course/UpdateCourse";
}

export class DepartmentURLConstants {
    static GET_ALL_DEPARTMENTS = apiUrl + "/api/course/GetAllDepartments";
}

export class LeadManagementURLConstants {
    static GET_ALL_LEADS = apiUrl + "/api/lead/GetAllLeads";
    static SAVELEAD = apiUrl + "/api/lead/SaveLead";
    static DELETELEAD = apiUrl + "/api/lead/";
    static GET_COURSES = apiUrl + "/api/course/GetCourses";
    static GETLEAD = apiUrl + "/api/lead/GetLead/";
    static DOWNLOAD_LEAD_LIST = apiUrl + "/api/lead/ExportLeads";
    static UPDATE_LEADS = apiUrl + "/api/lead/UpdateLeads";
    static GET_LEAD_BY_ID_URL = apiUrl + "/api/lead/GetLeadById";
    static GET_ENQUIRYTYPES = apiUrl + "/api/lead/GetEnquiryTypes";
    static GET_LEADSTATUS = apiUrl + "/api/lead/GetLeadStatus";
    static UPLOAD_FILE = apiUrl + "/api/lead/ImportLeads";
    static GET_LEAD_ACTIVITES = apiUrl + "/api/lead/GetLeadActivities/";
    static SAVE_LEAD_ACTIVITY = apiUrl + "/api/lead/SaveLeadActivity";
    static GET_ALL_COUNSELORS = apiUrl + "/api/user/GetAllCounselors";
    static UPDATE_COUNSELOR_BY_LEAD =
        apiUrl + "/api/lead/UpdateCounselorByLeadId";
    static SAVE_LEAD_COMMENT = apiUrl + "/api/lead/UpdateLeadComment";
    static SAVE_LEAD_SMS = apiUrl + "/api/lead/SaveLeadSms";
}

export class MarketingURLConstants {
    static UPLOAD_FILE = apiUrl + "/api/template/UploadEmailTemplate";
    static GET_ALL_CAMPAIGNS = apiUrl + "/api/campaign/GetAllCampaigns";
    static SAVE_SMS_TEMPLATE = apiUrl + "/api/template/SaveSmsTemplate";
    static ADD_CAMPAIGN_URL = apiUrl + "/api/campaign/SaveCampaign";
    static GET_CAMPAIGNTYPES = apiUrl + "/api/campaign/GetCampaignTypes";
    static DELETE_CAMPAIGN_URL = apiUrl + "/api/campaign/";
    static GET_MESSAGE_TYPE = apiUrl + "/api/template/GetMessageTypes";
    static GET_CAMPAIGN_BY_ID_URL = apiUrl + "/api/campaign/GetCampaignById/";
    static UPDATE_CAMPAIGN = apiUrl + "/api/campaign/UpdateCampaign";
    static GET_ALL_TEMPLATES = apiUrl + "/api/template/GetAllTemplates";
    static GET_ALL_SMS_TEMPLATES = apiUrl + "/api/template/GetAllSmsTemplates";

}

export class LeadSetURLConstants {
    static GET_ALL_LEAD_SET = apiUrl + "/api/leadSet/GetAllLeadSet";
    static DELETE_LEAD_SET = apiUrl + "/api/leadSet/";
    static GET_LEAD_SETS = apiUrl + "/api/leadSet/GetLeadSets";
    static SAVE_LEAD_SET = apiUrl + "/api/leadSet/SaveLeadSet";
    static UPDATE_LEAD_SET = apiUrl + "/api/leadSet/UpdateLeadSet";
    static GET_LEAD_SET_BY_ID = apiUrl + "/api/leadset/GetLeadSetById";
}
export class SmsTemplateURLConstants {
    static LEAD_SAVE_SMS = apiUrl + "/api/lead/SaveLeadSms";
    static GET_LEAD_SETS = apiUrl + "/api/leadSet/GetLeadSets";
    static GET_PERSONALIZATIONS_URL = apiUrl + "/api/Common/GetPersonalizations";
}

export class EmailTemplateURLConstants {
    static SAVE_LEAD_EMAIL_TEMPLATE = apiUrl + "/api/lead/SaveLeadEmail";
    static SAVE_CAMPAIGN_EMAIL_TEMPLATE =
        apiUrl + "/api/campaign/SaveCampaigmEmail";

    static GET_PERSONALIZATIONS_URL = apiUrl + "/api/Common/GetPersonalizations";
}
