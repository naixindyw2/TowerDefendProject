﻿//THIS FILE IS GENERATED BY tabtool, DO NOT EDIT IT!
//GENERATE TIME [2018/10/25 20:15:13]
#include "tableconfig.h"

bool LoadTableConfig()
{
    stConfigScope scope;
    if(false == Singleton<cfgAchiveTable>::Instance()->Load())
    {
        ErrorLog("加载配置%s出错",Singleton<cfgAchiveTable>::Instance()->GetTableFile().c_str() );
        return false;
    }
    return true;
}

bool cfgAchiveTable::Load()
{
	ReadTableFile reader;
	reader.Initialize();

	if (!reader.Init(GetTableFile().c_str()))
		return false;

	DataReader dr;
	int iRows = reader.GetRowCount();
	int iCols = reader.GetColCount();

	for (int i = 1; i < iRows; ++i)
	{
		tbsAchiveItem item;
		item.id = atoi(reader.GetValue(i, "id"));
		item.line = atoi(reader.GetValue(i, "line"));
      item.test1 = dr.GetObject<tbsIdCount>(reader.GetValue(i, "test1"));
		item.test2 = dr.GetIntList(reader.GetValue(i, "test2"));
      item.test3 = dr.GetObjectList<tbsKeyValue>(reader.GetValue(i, "test3"));
		m_Items[item.id] = item;
	}

	return true;
}