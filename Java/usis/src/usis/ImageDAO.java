package usis;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import to.ImageTO;

public class ImageDAO {
	
	public static ImageTO getImageWithId(String id) {
		ImageTO image = new ImageTO();
		try {
			Connection connection = Util.getConnection();
			Statement imageStatement = connection.createStatement();
			String imageSQL = "select id,url,title,screen_id from images where id=" +id;
			ResultSet imageResultSet = imageStatement.executeQuery(imageSQL);
			
			while (imageResultSet.next()) {
				image.setTitle(imageResultSet.getString("title"));
				image.setUrl(imageResultSet.getString("url"));
			}
			Util.closeConnection(connection, imageStatement, imageResultSet);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return image;
	}
	
	public static List<Images> getListofImages(String screenId) {
		List<Images> imageList = new ArrayList<Images>();
		try {
			Connection connection=Util.getConnection();
			Statement imagesStatement = connection.createStatement();
			String imageSQL = "select * from images where screen_id="+screenId+ " order by id";
			// System.out.println("imageSQL:"+imageSQL);
			ResultSet imagesResultset = imagesStatement.executeQuery(imageSQL);
			while (imagesResultset.next()) {
				Images image = new Images();
				image.setTitle(imagesResultset.getString("title"));
				image.setUrl(imagesResultset.getString("url"));
				imageList.add(image);
			}
			Util.closeConnection(null, imagesStatement, imagesResultset);
		} catch (Exception ee) {
			ee.printStackTrace();
		}
		return imageList;
	}
	
	
	
}
