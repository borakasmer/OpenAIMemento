using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI;
using MementoAI;
using OpenAI.ObjectModels.ResponseModels;

string tags = "Contains Credit and Car Tags.";
string prompt = "Write an email to Bora Kasmer offering the recipient a chance to win a $50\\r\\nAmazon gift card when they click a link to complete an employee satisfaction survey. Let the connection path be https://borakasmer.com?id=5. Create a beautifully formatted output using HTML and CSS.\", string whatyouare = \"You are a helpful assistant";
string whatyouare = "You are a helpful assistant.";

MementoAIPhishing memento = new();
//memento.MailTemplate = "1";
//memento.Save();
//memento.MailTemplate = "2";
//memento.Save();
//memento.MailTemplate = "3";
for (var i = 0; i < 3; i++)
{
    var result = GetPhishingMail(tags, prompt, whatyouare);
    memento.MailTemplate = result;
    if (i != 2) memento.Save();
}
Console.WriteLine(memento.MailTemplate);
Console.WriteLine("");
Console.WriteLine("Undo".PadLeft(40, '*'));
memento.Undo();
Console.WriteLine(memento.MailTemplate);
Console.WriteLine("Undo".PadLeft(40, '*'));
memento.Undo();
Console.WriteLine(memento.MailTemplate);
Console.WriteLine("Redo, Redo".PadLeft(40, '*'));
memento.Redo();
memento.Redo();
Console.WriteLine(memento.MailTemplate);

//Test For Null State
memento.Redo();
Console.WriteLine(String.IsNullOrEmpty(memento.MailTemplate)?"No Result": memento.MailTemplate);

Console.ReadLine();
string GetPhishingMail(string tags, string prompt, string whatyouare)
{
    var openAiService = new OpenAIService(new OpenAiOptions()
    {
        ApiKey = "---YOUR PASSWORD---"
    });

    var completionResult = openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest()
    {
        Messages = new List<ChatMessage>
        {
            ChatMessage.FromSystem(whatyouare),
            ChatMessage.FromUser(prompt),
            ChatMessage.FromUser(tags),

        },

        Model = Models.Gpt_4_turbo,
        MaxTokens = 2000,
        FrequencyPenalty = 0,
        Temperature = (float?)0.7,
        PresencePenalty = 0,
    });

    if (completionResult.Result.Successful)
    {
        return completionResult.Result.Choices.FirstOrDefault().Message.Content;
    }
    else
    {
        if (completionResult.Result.Error == null)
        {
            throw new Exception("Unknown Error");
        }
        return $"{completionResult.Result.Error.Code}: {completionResult.Result.Error.Message}";
    }
}