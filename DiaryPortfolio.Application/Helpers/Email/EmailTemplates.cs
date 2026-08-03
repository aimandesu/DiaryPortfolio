namespace DiaryPortfolio.Application.Helpers.Email
{
    public static class EmailTemplates
    {
        public static (string Subject, string Html) ConfirmationEmail(string confirmationLink)
        {
            const string subject = "Confirm your email";
            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;">
                    <h2>Confirm your email</h2>
                    <p>Thanks for signing up for Diary Portfolio. Click the button below to confirm your email address.</p>
                    <p style="margin: 24px 0;">
                        <a href="{confirmationLink}"
                           style="background-color: #059669; color: #ffffff; padding: 12px 20px; text-decoration: none; border-radius: 6px; display: inline-block;">
                            Confirm email
                        </a>
                    </p>
                    <p>If the button doesn't work, copy and paste this link into your browser:</p>
                    <p><a href="{confirmationLink}">{confirmationLink}</a></p>
                    <p>If you didn't create this account, you can safely ignore this email.</p>
                </div>
                """;
            return (subject, html);
        }

        public static (string Subject, string Html) PasswordResetEmail(string resetLink)
        {
            const string subject = "Reset your password";
            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 480px; margin: 0 auto;">
                    <h2>Reset your password</h2>
                    <p>We received a request to reset your Diary Portfolio password. Click the button below to choose a new one.</p>
                    <p style="margin: 24px 0;">
                        <a href="{resetLink}"
                           style="background-color: #059669; color: #ffffff; padding: 12px 20px; text-decoration: none; border-radius: 6px; display: inline-block;">
                            Reset password
                        </a>
                    </p>
                    <p>If the button doesn't work, copy and paste this link into your browser:</p>
                    <p><a href="{resetLink}">{resetLink}</a></p>
                    <p>If you didn't request a password reset, you can safely ignore this email.</p>
                </div>
                """;
            return (subject, html);
        }
    }
}
