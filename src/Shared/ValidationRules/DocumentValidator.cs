namespace DentallApp.Shared.ValidationRules;

public static class DocumentValidator
{
    public static IRuleBuilderOptions<T, string> MustBeValidDocument<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IIdentityDocumentValidator documentValidator)
    {
        return ruleBuilder.Must((rootObject, document, context) =>
        {
            if (string.IsNullOrWhiteSpace(document))
            {
                context.AddFailure(Messages.DocumentIsEmpty);
                return false;
            }

            Result result = documentValidator.IsValid(document);
            if (result.IsSuccess)
                return true;

            context.AddFailure(result.Message);
            foreach (string error in result.Errors)
                context.AddFailure(error);

            return false;
        });
    }
}
