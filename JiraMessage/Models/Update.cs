using Newtonsoft.Json;

namespace JiraMessage
{
    public class Update
    {  

        [JsonProperty("Changelog")]
        public Changelog Changelog { get; set; }

        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("Issue")]
        public Issue Issue { get; set; }

        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }

        [JsonIgnore]
        public UpdateType Type
        {
            get
            {   
                var webhookEvent = WebhookEvent;
                var issueEventTypeName = IssueEventTypeName;
                UpdateType updateType = UpdateType.None;                
                switch(webhookEvent)
                {
                    case "jira:issue_created":
                        updateType = UpdateType.IssueCreated;
                        break;
                    case "jira:issue_deleted":
                        updateType = UpdateType.IssueDeleted;
                        break;
                    case "jira:issue_updated":
                        switch(issueEventTypeName)
                        {                                                       
                            case "issue_commented":
                                updateType = UpdateType.CommentCreated;
                                break;
                            case "issue_comment_edited":
                                updateType = UpdateType.CommentUpdated;
                                break;
                            case "issue_comment_deleted":
                                updateType = UpdateType.CommentDeleted;
                                break;
                            default:
                                updateType = UpdateType.IssueUpdated;
                                break;
                        }
                        break;
                    //case "comment_created":
                    //    updateType = UpdateType.CommentCreated;
                    //    break;
                    //case "comment_deleted":
                    //    updateType = UpdateType.CommentDeleted;
                    //    break;
                    default:
                        break;
                }
                return updateType;
            }
        }

        [JsonProperty("User")]
        public User User { get; set; }

        [JsonProperty("webhookEvent")]
        public string WebhookEvent { get; set; }
        [JsonProperty("issue_event_type_name")]
        public string IssueEventTypeName { get; set; }
    }
}