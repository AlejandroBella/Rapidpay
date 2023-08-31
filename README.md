# Rapidpay

Includes an API o execute Card operaions CRUS and Payments as well as balance operaions, like remove a record

It is secure using Idenity Server 4 with in memory credentials using Credential Flow
    POST  <https://localhost:5001/connect/token>

    *mandatory*
    Request Headers:
    {
    "Content-Type": "application/x-www-form-urlencoded"
    }
    Request Body:
    {
    "client_id": "RapidPay.Client",
    "client_secret": "rapidSecret",
    "scope": "RapidPay.Card",
    "grant_type": "client_credentials"
    }

Uses a local db wiht EF7 and code first.