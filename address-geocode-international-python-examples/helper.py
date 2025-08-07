"""
helper.py

Utility functions for converting SOAP response objects to dictionaries.
"""

def response_to_dict(resp_obj):
    """
    Convert a SOAP response object into a nested dictionary.

    Args:
        resp_obj: The SOAP response object with a `.Response` iterable attribute.

    Returns:
        dict: A mapping from response keys to their flattened result dictionaries.
    """
    out = {}
    for resp in resp_obj.Response:
        key = resp.Key
        result = {}
        for field_obj in resp.Value.Result:
            for field in field_obj.Field:
                result[field.Key] = field.Value
        out[key] = result
    return out
