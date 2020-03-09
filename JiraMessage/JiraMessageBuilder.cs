using BotMessage;
using System;
using System.Linq;

namespace JiraMessage
{
    public class JiraMessageBuilder : BotMessageBuilder
    {
        private readonly string _issueKey;
        private readonly string _projectKey;
        private readonly Update _update;

        public JiraMessageBuilder(Update update, string projectKey, string issueKey,
            IBotMessageFormatter messageFormatter) : base(messageFormatter)
        {
            _update = update;
            _projectKey = projectKey;
            _issueKey = issueKey;
        }

        public new string Build()
        {
            switch (_update.Type)
            {
                case UpdateType.IssueCreated:
                case UpdateType.IssueUpdated:
                case UpdateType.IssueDeleted:
                    BuildIssueMessage();
                    break;
                case UpdateType.CommentCreated:
                case UpdateType.CommentUpdated:
                case UpdateType.CommentDeleted:
                    BuildCommentMessage();
                    break;
            }

            return base.Build();
        }


        private void BuildIssueMessage()
        {
            var issue = _update.Issue;
            AppendIssueTitle(issue);
            NewLine();

            AppendDescription(issue);
            NewLine();
            if (AppendChanges())
                NewLine();
            if (AppendPriority(issue))
                NewLine();
            if (AppendReporter(issue))
                NewLine();

            if (AppendAssignee(issue))
                NewLine();
        }

        private bool AppendChanges()
        {
            if (_update.Type != UpdateType.IssueUpdated) return false;
            foreach (var item in _update.Changelog.Items)
            {
                var fieldName = char.ToUpper(item.Field[index: 0]) + item.Field.Substring(startIndex: 1);


                NewLine();
                AppendBold(fieldName, escape: true);


                if (fieldName == "Attachment")
                {
                    if (string.IsNullOrEmpty(item.OldString))
                    {
                        var attachmentId = int.Parse(item.New);
                        var attachment = _update.Issue.Fields.Attachments.First(x => x.Id == attachmentId);
                        Append(" added ");
                        AppendLink(attachment.Content, attachment.FileName);
                    }
                    else
                    {
                        Append(" removed ");
                        AppendItalic($"\"{item.OldString}\"", escape: true);
                    }
                }
                else
                {
                    Append(" changed from ");
                    AppendItalic($"\"{item.OldString}\"", escape: true);
                    Append("  to ");
                    AppendItalic($"\"{item.NewString}\"", escape: true);
                }
            }
            return true;
        }

        private bool AppendAssignee(Issue issue)
        {
            if (issue.Fields.Assignee == null) return false;
            AppendBold("Assignee: ");
            Append(issue.Fields.Assignee.DisplayName, escape: true);
            return true;
        }

        private bool AppendReporter(Issue issue)
        {
            if (issue.Fields.Reporter == null) return false;
            AppendBold("Reporter: ");
            Append(issue.Fields.Reporter.DisplayName, escape: true);
            return true;
        }

        private bool AppendPriority(Issue issue)
        {
            var prio = issue.Fields.Priority;
            if (prio == null) return false;
            AppendBold("Priority: ");
            Append(prio.Name, escape: true);
            return true;
        }

        private void BuildCommentMessage()
        {
            var comment = _update.Comment;
            AppendBold(comment.Author.DisplayName, escape: true);
            Append(" commented on ");
            AppendIssueLink(comment.CommentUri, _issueKey, _update.Issue.Fields.Summary);
            NewLine();
            AppendCommentBody();
        }

        private void AppendIssueTitle(Issue issue)
        {
            AppendBold(issue.Fields.IssueType.Name, escape: true);
            Append(" ");
            AppendIssueLink(issue.IssueUri, _issueKey, issue.Fields.Summary);
            StartBold();
            Append(" was ");
            Append(_update.Type.ToString().Replace("Issue", string.Empty).ToLower(), escape: true);
            Append(" by");
            EndBold();
            Append(" ");
            AppendItalic(_update.User.DisplayName, escape: true);
        }

        private void AppendDescription(Issue issue)
        {
            AppendBold(issue.Fields.Summary, escape: true);
            if (_update.Type != UpdateType.IssueCreated) return;
            NewLine();
            Append(issue.Fields.Description, escape: true);
        }


        private void AppendIssueLink(Uri selfUri, string issueKey)
        {
            var issueUrl = new Uri(selfUri.Scheme + "://" + selfUri.Host +
                                   (selfUri.IsDefaultPort ? "" : ":" + selfUri.Port) + "/browse/" +
                                   issueKey).ToString();

            AppendLink(issueUrl, $"[{issueKey}]");
        }
        private void AppendIssueLink(Uri selfUri, string issueKey, string issueTitle)
        {
            var issueUrl = new Uri(selfUri.Scheme + "://" + selfUri.Host +
                                   (selfUri.IsDefaultPort ? "" : ":" + selfUri.Port) + "/browse/" +
                                   issueKey).ToString();

            AppendLink(issueUrl, $"[{issueKey}]:" + issueTitle);
        }

        private void AppendCommentBody()
        {
            //Todo parase formatting
            Append(_update.Comment.Body, escape: true);
        }
    }
}